'use client'

import { useMemo, useState } from 'react'
import { BarChart3, Bell, BookOpen, Building2, CalendarCheck, Check, ChevronDown, ClipboardList, FileText, GraduationCap, LayoutDashboard, Lock, LogOut, Menu, Pencil, Plus, Search, Settings, ShieldCheck, Trash2, Upload, UserCog, Users, X } from 'lucide-react'

type Profile = 'landing' | 'admin' | 'municipal' | 'school' | 'teacher'
type AdminView = 'dashboard' | 'municipalities' | 'activity'
type MunicipalView = 'dashboard' | 'schools' | 'directors' | 'reports'
type SchoolView = 'dashboard' | 'teachers' | 'classes' | 'students' | 'reports'
type TeacherView = 'dashboard' | 'attendance' | 'lessons' | 'grades' | 'planning'

// Contexto Global de Dados
interface School {
  id: string
  name: string
  director: string
  teachers: number
  students: number
  frequency: number
}

interface Municipality {
  schools: School[]
  directors: { name: string; school: string }[]
}

interface Teacher {
  id: string
  name: string
  classes: TeacherClass[]
}

interface TeacherClass {
  id: string
  name: string
  students: number
  frequency: number
}

// Estado Global Simulado
const defaultMunicipality: Municipality = {
  schools: [
    { id: '1', name: 'EMEF Professor João Ribeiro', director: 'Maria Silva', teachers: 24, students: 648, frequency: 93.8 },
    { id: '2', name: 'EMEF Aracê', director: 'João Santos', teachers: 18, students: 520, frequency: 91.2 },
    { id: '3', name: 'EMEF Cidade Satélite', director: 'Ana Costa', teachers: 20, students: 580, frequency: 92.5 },
  ],
  directors: [
    { name: 'Maria Silva', school: 'EMEF Professor João Ribeiro' },
    { name: 'João Santos', school: 'EMEF Aracê' },
    { name: 'Ana Costa', school: 'EMEF Cidade Satélite' },
  ]
}

const defaultTeacher: Teacher = {
  id: '1',
  name: 'Ana Paula Mendes',
  classes: [
    { id: '1', name: '5º Ano A', students: 32, frequency: 94.2 },
    { id: '2', name: '5º Ano B', students: 30, frequency: 92.8 },
  ]
}

const bahiaMunicipalities = 'Abaré,Acajutiba,Adustina,Água Fria,Érico Cardoso,Alagoinhas,Alcobaça,Almadina,Amargosa,Amélia Rodrigues,América Dourada,Anagé,Andaraí,Andorinha,Angical,Anguera,Antas,Antônio Cardoso,Antônio Gonçalves,Aporá,Apuarema,Aracatu,Araci,Aramari,Arataca,Aratuípe,Aurelino Leal,Baianópolis,Baixa Grande,Banzaê,Barra,Barra da Estiva,Barra do Choça,Barra do Mendes,Barra do Rocha,Barreiras,Barro Alto,Barro Preto,Barrocas,Belmonte,Belo Campo,Biritinga,Boa Nova,Boa Vista do Tupim,Bom Jesus da Lapa,Bom Jesus da Serra,Boninal,Bonito,Boquira,Botuporã,Brejões,Brejolândia,Brotas de Macaúbas,Brumado,Buerarema,Buritirama,Caatiba,Cachoeira,Caculé,Caém,Caetanos,Caetité,Cafarnaum,Cairu,Caldeirão Grande,Camacan,Camaçari,Camamu,Campo Alegre de Lourdes,Campo Formoso,Canápolis,Canarana,Canavieiras,Candeal,Candeias,Candiba,Cândido Sales,Cansanção,Canudos,Capela do Alto Alegre,Capim Grosso,Caravelas,Cardeal da Silva,Carinhanha,Casa Nova,Castro Alves,Catolândia,Catu,Caturama,Central,Chorrochó,Cícero Dantas,Cipó,Coaraci,Cocos,Conceição da Feira,Conceição do Almeida,Conceição do Coité,Conceição do Jacuípe,Conde,Condeúba,Contendas do Sincorá,Coração de Maria,Cordeiros,Coribe,Coronel João Sá,Correntina,Cotegipe,Cravolândia,Crisópolis,Cristópolis,Cruz das Almas,Curaçá,Dário Meira,Dias d’Ávila,Dom Basílio,Dom Macedo Costa,Elísio Medrado,Entre Rios,Esplanada,Euclides da Cunha,Eunápolis,Fátima,Feira de Santana,Filadélfia, Firmino Alves,Floresta Azul,Formosa do Rio Preto,Gandu,Gavião,Gentio do Ouro,Glória,Gongogi,Governador Mangabeira,Guajeru,Guanambi,Guaratinga,Heliópolis,Iaçu,Ibiassucê,Ibicaraí,Ibicoara,Ibicuí,Ibirapitanga,Ibirapuã,Ibirataia,Ibotirama,Ichu,Igrapiúna,Ilhéus,Inhambupe,Ipecaetá,Ipiaú,Ipirá,Irajuba,Iramaia,Iraquara,Irecê,Itaberaba,Itabuna,Itacaré,Itaeté,Itagi,Itagibá,Itagimirim,Itaguaçu da Bahia,Itaju do Colônia,Itajuípe,Itamaraju,Itamari,Itambé,Itanagra,Itanhém,Itaparica,Itapé,Itapebi,Itapetinga,Itapicuru,Itapitanga,Itaquara,Itarantim,Itatim,Itiruçu,Itiúba,Itororó,Ituaçu,Ituberá,Jaborandi,Jacaraci,Jacobina,Jaguaquara,Jaguarari,Jaguaripe,Jandaíra,Jequié,Jiquiriçá,Jeremoabo,João Dourado,Juazeiro,Jucuruçu,Jussara,Jussari,Jussiape,Lafaiete Coutinho,Lagoa Real,Laje,Lajedão,Lajedo do Tabocal,Lamarão,Lapão,Lauro de Freitas,Lençóis,Licínio de Almeida,Livramento de Nossa Senhora,Luís Eduardo Magalhães,Macajuba,Macarani,Macaúbas,Macururé,Madre de Deus,Maetinga,Maiquinique,Mairi,Malhada,Malhada de Pedras,Manoel Vitorino,Mansidão,Maracás,Maragogipe,Maraú,Marcionílio Souza,Mascote,Mata de São João,Matina,Medeiros Neto,Miguel Calmon,Milagres,Mirangaba,Mirante,Monte Santo,Morro do Chapéu,Mortugaba,Mucugê,Mucuri,Muquém do São Francisco,Mundo Novo,Muniz Ferreira,Muquém de São Francisco,Nazaré,Nilo Peçanha,Nordestina,Nova Canaã,Nova Fátima,Nova Itarana,Nova Redenção,Nova Soure,Novo Horizonte,Novo Triunfo,Olindina,Oliveira dos Brejinhos,Ouriçangas,Ourolândia,Palmas de Monte Alto,Palmeiras,Paramirim,Paratinga,Paripiranga,Pau Brasil,Paulo Afonso,Pé de Serra,Pedro Alexandre,Piata,Pilão Arcado,Pindaí,Pindobaçu,Pintadas, Piraí do Norte, Piripá, Piritiba, Planaltino,Planalto,Poções,Pojuca,Ponto Novo,Porto Seguro,Potiraguá,Prado,Presidente Dutra,Presidente Jânio Quadros,Presidente Tancredo Neves,Queimadas,Quijingue,Quixabeira,Rafael Jambeiro,Remanso,Retiro de Santana,Riachão das Neves,Riachão do Jacuípe,Riacho de Santana,Ribeira do Amparo,Ribeira do Pombal,Ribeirão do Largo,Rio de Contas,Rio do Antônio,Rio do Pires,Rodelas,Ruy Barbosa,Salinas da Margarida,Salvador,Santa Bárbara,Santa Brígida,Santa Cruz Cabrália,Santa Cruz da Vitória,Santa Inês,Santa Luzia,Santa Maria da Vitória,Santa Rita de Cássia,Santa Teresinha,Santaluz,Santanópolis,Santo Amaro,Santo Antônio de Jesus,Santo Estêvão,São Desidério,São Felipe,São Félix,São Félix do Coribe,São Francisco do Conde,São Gabriel,São Gonçalo dos Campos,São José da Vitória,São José do Jacuípe,São Miguel das Matas,São Sebastião do Passé,Sapeaçu,Sátiro Dias,Saubara,Saúde,Seabra,Sebastião Laranjeiras,Senhor do Bonfim,Sento Sé,Serra do Ramalho,Serra Dourada,Serra Preta,Serrinha,Serrolândia,Simões Filho,Sítio do Mato,Sítio do Quinto,Sobradinho,Souto Soares,Tabocas do Brejo Velho,Tanque Novo,Tanquinho,Taperoá,Tapiramutá,Teixeira de Freitas,Teodoro Sampaio,Teofilândia,Teolândia,Tremedal,Tucano,Uauá,Ubaíra,Ubatã,Uibaí,Una,Urandi,Uruçuca,Utinga,Valença,Valente,Várzea da Roça,Várzea do Poço,Várzea Nova,Varzedo,Vereda,Vera Cruz,Vitória da Conquista,Wagner,Wanderley,Wenceslau Guimarães,Xique-Xique'.split(',').map((item) => item.trim()).sort((a, b) => a.localeCompare(b, 'pt-BR'))

interface MunicipalSecretary { municipality: string; name: string; phone: string; email: string }

const defaultSecretaries: MunicipalSecretary[] = []

export default function Page() {
  const [profile, setProfile] = useState<Profile>('landing')
  const [adminAuthenticated, setAdminAuthenticated] = useState(false)
  const [municipality, setMunicipality] = useState<Municipality>(defaultMunicipality)
  const [municipalities, setMunicipalities] = useState(['Secretaria Municipal de Educação de Aurora', 'Secretaria Municipal de Educação de Vale Verde'])
  const [currentSchool, setCurrentSchool] = useState<School | null>(null)
  const [currentTeacher] = useState<Teacher>(defaultTeacher)

  const handleSelectSchool = (school: School) => {
    setCurrentSchool(school)
    setProfile('school')
  }

  const handleSchoolAccess = () => {
    setCurrentSchool(municipality.schools[0])
    setProfile('school')
  }

  const handleTeacherAccess = () => {
    setProfile('teacher')
  }

  if (profile === 'admin') {
    if (!adminAuthenticated) {
      return <AdminLogin onSuccess={() => setAdminAuthenticated(true)} onBack={() => setProfile('landing')} />
    }
    return <Admin municipalities={municipalities} setMunicipalities={setMunicipalities} onBack={() => { setAdminAuthenticated(false); setProfile('landing') }} />
  }

  if (profile === 'municipal') {
    return <Municipal municipality={municipality} setMunicipality={setMunicipality} onSelectSchool={handleSelectSchool} onBack={() => setProfile('landing')} />
  }

  if (profile === 'school' && currentSchool) {
    return <SchoolApp school={currentSchool} municipality={municipality} onBack={() => setProfile('landing')} />
  }

  if (profile === 'teacher') {
    return <TeacherApp teacher={currentTeacher} onBack={() => setProfile('landing')} />
  }

  return <Landing openAdmin={() => setProfile('admin')} openMunicipal={() => setProfile('municipal')} openSchool={handleSchoolAccess} openTeacher={handleTeacherAccess} />
}

// ============ COMPONENTES COMPARTILHADOS ============
function parseCsv(text: string) {
  return text.trim().split(/\r?\n/).slice(1).map((row) => row.split(/[;,]/)[0]?.trim()).filter(Boolean)
}

function downloadCsvTemplate(fileName: string, headers: string[], sample: string[]) {
  const csv = [headers.join(';'), sample.join(';')].join('\n')
  const blob = new Blob([`\\ufeff${csv}`], { type: 'text/csv;charset=utf-8;' })
  const url = URL.createObjectURL(blob)
  const link = document.createElement('a')
  link.href = url
  link.download = fileName
  link.click()
  URL.revokeObjectURL(url)
}

function CsvUpload({ label, onImport, template }: { label: string; onImport: (items: string[]) => void; template: { fileName: string; headers: string[]; sample: string[] } }) {
  const [message, setMessage] = useState('')
  const handleFile = (event: React.ChangeEvent<HTMLInputElement>) => {
    const file = event.target.files?.[0]
    if (!file) return
    const reader = new FileReader()
    reader.onload = () => {
      const items = parseCsv(String(reader.result || ''))
      onImport(items)
      setMessage(`${items.length} registro(s) importado(s)`)
    }
    reader.readAsText(file, 'UTF-8')
    event.target.value = ''
  }
  return <div className="flex flex-wrap items-center gap-2">
    <button type="button" onClick={() => downloadCsvTemplate(template.fileName, template.headers, template.sample)} className="rounded-xl border border-[#ead9b8] bg-white px-3 py-3 text-xs font-bold text-[#64738d] hover:border-[#9fbeff] hover:text-[#afab50]">Baixar modelo CSV</button>
    <label className="flex cursor-pointer items-center justify-center gap-2 rounded-xl border border-[#f3d49e] bg-[#fff7e9] px-4 py-3 text-sm font-bold text-[#afab50] hover:bg-[#fff0d8]"><Upload size={16} /> {label}<input type="file" accept=".csv,text/csv,.xlsx,application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" onChange={handleFile} className="sr-only" />{message && <span className="text-xs font-semibold text-[#21a579]">{message}</span>}</label>
  </div>
}

function Brand() {
  return (
    <div className="flex items-center gap-3">
      <img
        src="https://con3ktar.nekoweb.org/logo.jpeg"
        alt="con3ktar"
        className="size-11 rounded-[14px] object-cover"
      />
      <span className="text-[21px] font-extrabold tracking-[-.04em]">Educa+</span>
    </div>
  )
}

function Landing({ openAdmin, openMunicipal, openSchool, openTeacher }: any) {
  return (
    <main className="min-h-screen bg-[#fffaf2] text-[#403d28]">
      <header className="border-b border-[#eee6d5] bg-white/90">
        <div className="mx-auto flex h-[78px] max-w-[1320px] items-center justify-between px-6 lg:px-10">
          <Brand />

        </div>
      </header>

      <section className="mx-auto grid max-w-[1320px] items-center gap-14 px-6 pb-20 pt-16 lg:grid-cols-[1.03fr_.97fr] lg:px-10 lg:pt-24">
        <div>
          <div className="mb-6 inline-flex rounded-full border border-[#f3d49e] bg-[#fff0d8] px-3.5 py-2 text-[11px] font-bold uppercase tracking-[.12em] text-[#2866d4]">
            Gestão que transforma a educação
          </div>
          <h1 className="max-w-[680px] text-[46px] font-extrabold leading-[1.08] tracking-[-.055em] sm:text-[60px]">
            A escola conectada com o <span className="text-[#afab50]">futuro.</span>
          </h1>
          <p className="mt-6 max-w-[570px] text-[17px] leading-8 text-[#69758a]">
            Uma plataforma completa para simplificar a gestão escolar, valorizar professores e potencializar a aprendizagem.
          </p>
        </div>

        <div className="rounded-[26px] border border-[#e4eaf3] bg-white p-5 shadow-[0_24px_60px_rgba(37,69,125,.1)] sm:p-7">
<div className="mb-6 flex items-end justify-between gap-4">
<div>
<p className="text-[11px] font-bold uppercase tracking-[.12em] text-[#8994a8]">Acesso rápido</p>
<h2 className="mt-1 text-[22px] font-extrabold">Escolha seu perfil</h2>
</div>
<button onClick={openAdmin} aria-label="Acesso Administrativo" title="Acesso Administrativo" className="grid size-9 shrink-0 place-items-center rounded-lg border border-[#d8d49b] bg-[#fff7e9] text-[#afab50] transition hover:bg-[#fcbf6b] hover:text-white">
<Lock size={16} aria-hidden="true" />
</button>
</div>
          <Access title="Secretaria Municipal" text="Gestão completa da rede municipal de ensino." icon={Building2} onClick={openMunicipal} active />
          <Access title="Secretaria da Escola" text="Organize professores, turmas e alunos." icon={Building2} onClick={openSchool} />
          <Access title="Professor" text="Mais tempo para ensinar. Menos burocracia." icon={GraduationCap} onClick={openTeacher} />
        </div>
      </section>
    </main>
  )
}

function Access({ title, text, icon: Icon, onClick, active = false }: any) {
  return (
    <button
      onClick={onClick}
      className={`mb-3 flex w-full items-center gap-4 rounded-[15px] border p-4 text-left transition ${
        active ? 'border-[#9fbeff] bg-[#f0f5ff]' : 'border-[#edf0f5] hover:border-[#cdd9ed]'
      }`}
    >
      <span className={`grid size-11 place-items-center rounded-[12px] ${active ? 'bg-[#afab50] text-white' : 'bg-[#f1f4f8] text-[#64738d]'}`}>
        <Icon size={20} />
      </span>
      <span className="flex-1">
        <span className="block text-[14px] font-bold">{title}</span>
        <span className="mt-1 block text-[11px] text-[#8490a4]">{text}</span>
      </span>
      <span className="text-[#9aa4b4]">→</span>
    </button>
  )
}

// ============ SHELL GENÉRICO ============
function Shell({ title, view, setView, items, onBack, accessScope, children }: any) {
  return (
    <div className="min-h-screen bg-[#fffaf2] text-[#403d28]">
      <aside className="fixed inset-y-0 left-0 z-20 hidden w-64 border-r border-[#eee6d5] bg-white p-5 lg:block">
        <Brand />
        <div className="mt-10 flex flex-col gap-2">
          {items.map(({ id, label, icon: Icon }: any) => (
            <button
              key={id}
              onClick={() => setView(id)}
              className={`flex items-center gap-3 rounded-xl px-3 py-3 text-sm font-semibold ${
                view === id ? 'bg-[#fff0d8] text-[#afab50]' : 'text-[#68768c] hover:bg-[#fffaf2]'
              }`}
            >
              <Icon size={18} />
              {label}
            </button>
          ))}
        </div>
        <button onClick={onBack} className="absolute bottom-5 left-5 flex items-center gap-3 px-3 py-3 text-sm font-semibold text-[#68768c]">
          <LogOut size={18} />
          Sair
        </button>
      </aside>

      <div className="lg:pl-64">
        <header className="flex h-[78px] items-center justify-between border-b border-[#eee6d5] bg-white px-6 lg:px-10">
          <div className="flex items-center gap-4">
            <button className="lg:hidden" aria-label="Abrir menu">
              <Menu />
            </button>
            <div>
              <p className="text-[11px] font-bold uppercase tracking-[.14em] text-[#afab50]">{title}</p>
              <h1 className="text-xl font-extrabold">{items.find((i: any) => i.id === view)?.label || 'Visão geral'}</h1>
            </div>
          </div>
          <div className="flex items-center gap-4">
            <Bell className="text-[#8490a4]" size={19} />
            <span className="hidden text-sm font-bold sm:block">Sistema educa+</span>
            <ChevronDown size={15} />
          </div>
        </header>
        <main className="p-6 lg:p-10">
          {accessScope && <div className="mb-6 flex items-start gap-3 rounded-2xl border border-[#ead9b8] bg-[#fff7e9] px-4 py-3 text-sm text-[#53637b]" role="status"><ShieldCheck className="mt-0.5 shrink-0 text-[#afab50]" size={18} /><div><p className="font-extrabold text-[#403d28]">Permissões deste perfil</p><p className="mt-1 leading-6">{accessScope}</p></div></div>}
          {children}
        </main>
      </div>
    </div>
  )
}

// ============ ACESSO DO PROPRIETÁRIO ============
function AdminLogin({ onSuccess, onBack }: { onSuccess: () => void; onBack: () => void }) {
  const [email, setEmail] = useState('')
  const [password, setPassword] = useState('')
  const [error, setError] = useState('')

  const submit = (event: React.FormEvent) => {
    event.preventDefault()
    if (email.trim().toLowerCase() === 'admin@educamais.com' && password === 'Educa@2026') {
      setError('')
      onSuccess()
      return
    }
    setError('E-mail ou senha inválidos. Use as credenciais demonstrativas abaixo.')
  }

  return (
    <main className="grid min-h-screen place-items-center bg-[#fffaf2] px-5 text-[#403d28]">
      <section className="w-full max-w-md rounded-[26px] border border-[#e4eaf3] bg-white p-7 shadow-[0_24px_60px_rgba(37,69,125,.1)] sm:p-9">
        <Brand />
        <div className="mt-9">
          <div className="mb-5 grid size-12 place-items-center rounded-2xl bg-[#fff0d8] text-[#afab50]"><ShieldCheck size={23} /></div>
          <p className="text-sm font-bold text-[#afab50]">Área restrita</p>
          <h1 className="mt-1 text-3xl font-extrabold tracking-[-.04em]">Acesso Administrativo</h1>
                </div>
        <form onSubmit={submit} className="mt-7 flex flex-col gap-4">
          <label className="flex flex-col gap-2 text-sm font-bold">E-mail<input required type="email" value={email} onChange={(event) => setEmail(event.target.value)} placeholder="admin@educamais.com" className="rounded-xl border border-[#ead9b8] px-4 py-3 font-normal outline-none focus:border-[#afab50]" /></label>
          <label className="flex flex-col gap-2 text-sm font-bold">Senha<input required type="password" value={password} onChange={(event) => setPassword(event.target.value)} placeholder="Digite sua senha" className="rounded-xl border border-[#ead9b8] px-4 py-3 font-normal outline-none focus:border-[#afab50]" /></label>
          {error && <p role="alert" className="rounded-xl bg-[#fff3f1] px-4 py-3 text-sm font-semibold text-[#c84c3f]">{error}</p>}
          <button type="submit" className="mt-2 rounded-xl bg-[#afab50] px-4 py-3.5 text-sm font-bold text-white hover:bg-[#1555c4]">Entrar no painel</button>
        </form>
        <div className="mt-5 rounded-xl bg-[#fffaf2] p-4 text-xs leading-5 text-[#748197]"><strong className="text-[#403d28]">Acesso demonstrativo:</strong><br />E-mail: admin@educamais.com<br />Senha: Educa@2026</div>
        <button type="button" onClick={onBack} className="mt-5 w-full text-sm font-bold text-[#748197] hover:text-[#afab50]">Voltar para a página inicial</button>
      </section>
    </main>
  )
}

// ============ PERFIL SUPERADMIN ============
function Admin({ municipalities, setMunicipalities, onBack }: any) {
  const [view, setView] = useState<AdminView>('dashboard')
  const [showForm, setShowForm] = useState(false)
  const [name, setName] = useState('')
  const [email, setEmail] = useState('')
  const [status, setStatus] = useState('Ativa')
  const [selectedMunicipality, setSelectedMunicipality] = useState('')
  const [secretaries, setSecretaries] = useState<MunicipalSecretary[]>(defaultSecretaries)
  const [secretaryForm, setSecretaryForm] = useState({ name: '', phone: '', email: '' })
  const items = [
    { id: 'dashboard', label: 'Visão geral', icon: LayoutDashboard },
    { id: 'municipalities', label: 'Secretarias municipais', icon: Building2 },
    { id: 'activity', label: 'Atividade do sistema', icon: BarChart3 },
  ]
  const addMunicipality = (event: any) => {
    event.preventDefault()
    if (!name.trim()) return
    setMunicipalities([...municipalities, name.trim()])
    setName('')
    setEmail('')
    setStatus('Ativa')
    setShowForm(false)
  }
  const editMunicipality = (index: number) => {
    const nextName = window.prompt('Nome da secretaria municipal:', municipalities[index])?.trim()
    if (!nextName) return
    setMunicipalities((current: string[]) => current.map((item: string, itemIndex: number) => itemIndex === index ? nextName : item))
  }
  const deleteMunicipality = (index: number) => {
    if (window.confirm('Excluir esta secretaria municipal?')) setMunicipalities((current: string[]) => current.filter((_: string, itemIndex: number) => itemIndex !== index))
  }
  const selectedSecretary = secretaries.find((item) => item.municipality === selectedMunicipality)
  const saveSecretary = (event: React.FormEvent) => {
    event.preventDefault()
    if (!selectedMunicipality || !secretaryForm.name.trim() || !secretaryForm.phone.trim() || !secretaryForm.email.trim()) return
    setSecretaries((current) => [...current.filter((item) => item.municipality !== selectedMunicipality), { municipality: selectedMunicipality, ...secretaryForm, name: secretaryForm.name.trim(), phone: secretaryForm.phone.trim(), email: secretaryForm.email.trim() }])
    setSecretaryForm({ name: '', phone: '', email: '' })
  }
  const selectMunicipality = (value: string) => {
    setSelectedMunicipality(value)
    const secretary = secretaries.find((item) => item.municipality === value)
    setSecretaryForm(secretary ? { name: secretary.name, phone: secretary.phone, email: secretary.email } : { name: '', phone: '', email: '' })
  }
  return (
    <Shell title="Superadmin" view={view} setView={setView} items={items} onBack={onBack} accessScope="Acesso total: Secretaria Municipal, Secretaria da Escola, Professor e todas as configurações do sistema.">
      {view === 'dashboard' && <>
        <div className="mb-8 flex flex-col justify-between gap-4 md:flex-row md:items-end">
          <div><p className="text-sm font-semibold text-[#afab50]">Controle central</p><h2 className="mt-1 text-3xl font-extrabold tracking-[-.04em]">Olá, proprietário.</h2><p className="mt-2 text-[#748197]">Gerencie a hierarquia de acessos de toda a plataforma.</p></div>
          <button onClick={() => { setView('municipalities'); setShowForm(true) }} className="flex items-center justify-center gap-2 rounded-xl bg-[#afab50] px-4 py-3 text-sm font-bold text-white"><Plus size={17} /> Cadastrar secretaria</button>
        </div>
        <div className="grid gap-4 md:grid-cols-3">
          <AdminStat label="Secretarias municipais" value={municipalities.length} detail="contas cadastradas" />
          <AdminStat label="Secretarias escolares" value="18" detail="gerenciadas pela rede" />
          <AdminStat label="Usuários ativos" value="246" detail="com acesso liberado" />
        </div>
        <div className="mt-6 rounded-2xl border border-[#eee6d5] bg-white p-6"><h3 className="font-extrabold">Fluxo de permissões</h3><div className="mt-5 grid gap-3 md:grid-cols-3"><PermissionStep n="01" title="Superadmin" text="Cadastra e acompanha secretarias municipais." /><PermissionStep n="02" title="Secretaria municipal" text="Cadastra escolas e seus responsáveis." /><PermissionStep n="03" title="Secretaria escolar" text="Cadastra professores e libera acessos." /></div></div>
      </>}
      {view === 'municipalities' && <div className="rounded-2xl border border-[#eee6d5] bg-white p-6"><div className="flex flex-col justify-between gap-4 sm:flex-row sm:items-center"><div><h2 className="text-xl font-extrabold">Secretarias municipais</h2><p className="mt-1 text-sm text-[#748197]">Cada secretaria terá autonomia sobre sua rede de escolas.</p></div><button onClick={() => setShowForm(!showForm)} className="flex items-center justify-center gap-2 rounded-xl bg-[#afab50] px-4 py-3 text-sm font-bold text-white"><Plus size={17} /> Nova secretaria</button></div>
      <div className="mt-6 rounded-2xl border border-[#f3d49e] bg-[#fffaf2] p-5"><div className="flex items-start gap-3"><Building2 className="mt-1 text-[#afab50]" size={20} /><div><h3 className="font-extrabold">Secretário municipal</h3><p className="mt-1 text-sm text-[#748197]">Selecione um município da Bahia para atribuir ou atualizar seu responsável.</p></div></div><label className="mt-4 flex flex-col gap-2 text-sm font-bold">Município<select value={selectedMunicipality} onChange={(event) => selectMunicipality(event.target.value)} className="rounded-xl border border-[#ead9b8] bg-white px-3 py-3 font-normal outline-none focus:border-[#afab50]"><option value="">Selecione o município</option>{bahiaMunicipalities.map((item) => <option key={item} value={item}>{item}</option>)}</select></label>{selectedMunicipality && <form onSubmit={saveSecretary} className="mt-4 grid gap-3 md:grid-cols-3"><input required value={secretaryForm.name} onChange={(event) => setSecretaryForm({ ...secretaryForm, name: event.target.value })} placeholder="Nome do secretário" className="rounded-xl border border-[#ead9b8] bg-white px-3 py-3 text-sm outline-none focus:border-[#afab50]" /><input required value={secretaryForm.phone} onChange={(event) => setSecretaryForm({ ...secretaryForm, phone: event.target.value })} placeholder="Telefone" type="tel" className="rounded-xl border border-[#ead9b8] bg-white px-3 py-3 text-sm outline-none focus:border-[#afab50]" /><input required value={secretaryForm.email} onChange={(event) => setSecretaryForm({ ...secretaryForm, email: event.target.value })} placeholder="E-mail" type="email" className="rounded-xl border border-[#ead9b8] bg-white px-3 py-3 text-sm outline-none focus:border-[#afab50]" /><button type="submit" className="rounded-xl bg-[#afab50] px-4 py-3 text-sm font-bold text-white md:col-span-3 md:justify-self-end">{selectedSecretary ? 'Atualizar secretário' : 'Atribuir secretário'}</button></form>}</div>{showForm && <form onSubmit={addMunicipality} className="mt-6 grid gap-3 rounded-xl bg-[#fffaf2] p-4 md:grid-cols-[1.5fr_1fr_auto]"><input value={name} onChange={e => setName(e.target.value)} placeholder="Nome da secretaria municipal" className="rounded-lg border border-[#dfe6f0] bg-white px-3 py-3 text-sm outline-none focus:border-[#afab50]" required /><input value={email} onChange={e => setEmail(e.target.value)} type="email" placeholder="E-mail do responsável" className="rounded-lg border border-[#dfe6f0] bg-white px-3 py-3 text-sm outline-none focus:border-[#afab50]" required /><button className="rounded-lg bg-[#403d28] px-5 py-3 text-sm font-bold text-white">Cadastrar</button></form>}<div className="mt-6 flex flex-col gap-3">{municipalities.map((item: string, index: number) => <div key={`${item}-${index}`} className="flex flex-col gap-3 rounded-xl border border-[#edf0f5] p-4 sm:flex-row sm:items-center"><span className="grid size-10 shrink-0 place-items-center rounded-xl bg-[#fff0d8] text-[#afab50]"><Building2 size={18} /></span><div className="flex-1"><p className="font-bold">{item}</p><p className="mt-1 text-xs text-[#8994a8]">Administrador municipal · acesso hierárquico</p></div><span className="rounded-full bg-[#eaf8f0] px-3 py-1 text-xs font-bold text-[#23834d]">Ativa</span><ActionButtons onEdit={() => editMunicipality(index)} onDelete={() => deleteMunicipality(index)} /></div>)}</div></div>}
      {view === 'activity' && <div className="rounded-2xl border border-[#eee6d5] bg-white p-6"><h2 className="text-xl font-extrabold">Atividade do sistema</h2><p className="mt-1 text-sm text-[#748197]">Acompanhe a evolução dos acessos por perfil.</p><div className="mt-6 flex flex-col gap-4">{[['Secretarias municipais', '12 novos acessos esta semana', '86%'], ['Secretarias escolares', '34 novos acessos esta semana', '71%'], ['Professores', '108 novos acessos esta semana', '94%']].map(([label, text, value]) => <div key={label}><div className="mb-2 flex justify-between text-sm"><span className="font-bold">{label}</span><span className="font-bold text-[#afab50]">{value}</span></div><div className="h-2 rounded-full bg-[#edf1f6]"><div className="h-2 rounded-full bg-[#afab50]" style={{ width: value }} /></div><p className="mt-2 text-xs text-[#8994a8]">{text}</p></div>)}</div></div>}
    </Shell>
  )
}

function AdminStat({ label, value, detail }: any) { return <div className="rounded-2xl border border-[#eee6d5] bg-white p-5"><p className="text-sm text-[#748197]">{label}</p><p className="mt-3 text-3xl font-extrabold">{value}</p><p className="mt-1 text-xs text-[#8994a8]">{detail}</p></div> }
function PermissionStep({ n, title, text }: any) { return <div className="rounded-xl border border-[#edf0f5] p-4"><span className="text-xs font-extrabold text-[#afab50]">{n}</span><h4 className="mt-3 font-extrabold">{title}</h4><p className="mt-1 text-xs leading-5 text-[#748197]">{text}</p></div> }

// ============ PERFIL SECRETARIA MUNICIPAL ============
function Municipal({ municipality, setMunicipality, onSelectSchool, onBack }: any) {
  const [view, setView] = useState<MunicipalView>('dashboard')
  const [showAddSchool, setShowAddSchool] = useState(false)
  const [newSchool, setNewSchool] = useState('')
  const [showAddDirector, setShowAddDirector] = useState(false)
  const [newDirector, setNewDirector] = useState({ name: '', email: '', school: '' })

  const items = [
    { id: 'dashboard', label: 'Visão geral', icon: LayoutDashboard },
    { id: 'schools', label: 'Escolas', icon: Building2 },
    { id: 'directors', label: 'Diretores', icon: Users },
    { id: 'reports', label: 'Relatórios', icon: FileText },
  ]

  const handleAddSchool = (e: any) => {
    e.preventDefault()
    if (newSchool.trim()) {
      const school: School = {
        id: Date.now().toString(),
        name: newSchool,
        director: 'Pendente',
        teachers: 0,
        students: 0,
        frequency: 0,
      }
      setMunicipality({ ...municipality, schools: [...municipality.schools, school] })
      setNewSchool('')
      setShowAddSchool(false)
    }
  }

  const importSchools = (items: string[]) => setMunicipality({ ...municipality, schools: [...municipality.schools, ...items.map((name, i) => ({ id: `csv-${Date.now()}-${i}`, name, director: 'Pendente', teachers: 0, students: 0, frequency: 0 }))] })
  const importDirectors = (items: string[]) => setMunicipality({ ...municipality, directors: [...municipality.directors, ...items.map((name) => ({ name, email: '', school: 'A definir' }))] })
  const editSchool = (id: string) => {
    const school = municipality.schools.find((item: School) => item.id === id)
    const name = school && window.prompt('Nome da escola:', school.name)?.trim()
    if (school && name) setMunicipality({ ...municipality, schools: municipality.schools.map((item: School) => item.id === id ? { ...item, name } : item) })
  }
  const deleteSchool = (id: string) => {
    if (window.confirm('Excluir esta escola e seus dados?')) setMunicipality({ ...municipality, schools: municipality.schools.filter((item: School) => item.id !== id) })
  }
  const editDirector = (index: number) => {
    const director = municipality.directors[index]
    const name = window.prompt('Nome do diretor:', director.name)?.trim()
    if (name) setMunicipality({ ...municipality, directors: municipality.directors.map((item: any, itemIndex: number) => itemIndex === index ? { ...item, name } : item), schools: municipality.schools.map((school: School) => school.director === director.name ? { ...school, director: name } : school) })
  }
  const deleteDirector = (index: number) => {
    if (window.confirm('Excluir este diretor?')) setMunicipality({ ...municipality, directors: municipality.directors.filter((_: any, itemIndex: number) => itemIndex !== index) })
  }

  const handleAddDirector = (e: any) => {
    e.preventDefault()
    if (!newDirector.name.trim() || !newDirector.email.trim() || !newDirector.school) return
    const director = { name: newDirector.name.trim(), email: newDirector.email.trim(), school: newDirector.school }
    setMunicipality({
      ...municipality,
      directors: [...municipality.directors, director],
      schools: municipality.schools.map((school: School) => school.name === director.school ? { ...school, director: director.name } : school),
    })
    setNewDirector({ name: '', email: '', school: '' })
    setShowAddDirector(false)
  }

  const totalStudents = municipality.schools.reduce((sum: number, s: School) => sum + s.students, 0)
  const totalTeachers = municipality.schools.reduce((sum: number, s: School) => sum + s.teachers, 0)
  const avgFrequency = (municipality.schools.reduce((sum: number, s: School) => sum + s.frequency, 0) / municipality.schools.length).toFixed(1)

  return (
    <Shell title="Secretaria Municipal" view={view} setView={setView} items={items} onBack={onBack} accessScope="Acesso à sua Secretaria Municipal, às Secretarias das Escolas da rede e aos Professores vinculados.">
      {view === 'dashboard' && (
        <>
          <div className="mb-8">
            <p className="text-sm text-[#8490a4]">Quarta-feira, 17 de setembro de 2026</p>
            <h2 className="mt-1 text-3xl font-extrabold tracking-[-.04em]">Bem-vindo à Rede Municipal</h2>
            <p className="mt-2 text-sm text-[#718097]">Acompanhe toda a rede de educação municipal.</p>
          </div>

          <div className="grid gap-4 sm:grid-cols-2 xl:grid-cols-4">
            <Card icon={Building2} value={municipality.schools.length} label="Escolas" />
            <Card icon={Users} value={totalTeachers} label="Professores" />
            <Card icon={GraduationCap} value={totalStudents} label="Alunos" />
            <Card icon={CalendarCheck} value={`${avgFrequency}%`} label="Frequência média" />
          </div>

          <div className="mt-6 grid gap-6 xl:grid-cols-[1.3fr_.7fr]">
            <div className="rounded-2xl border border-[#eee6d5] bg-white p-6">
              <div className="flex justify-between">
                <div>
                  <h3 className="font-extrabold">Frequência por escola</h3>
                  <p className="mt-1 text-xs text-[#8490a4]">Últimos dados coletados</p>
                </div>
                <BarChart3 className="text-[#afab50]" size={20} />
              </div>
              <div className="mt-8 flex h-48 items-end gap-2">
                {municipality.schools.map((school: School) => (
                  <div key={school.id} className="flex flex-1 flex-col items-center gap-2">
                    <div className="w-full rounded-t-lg bg-[#fcbf6b]" style={{ height: `${school.frequency * 1.6}px`, maxHeight: 150 }} />
                    <span className="text-[10px] text-[#9aa4b4] text-center truncate">{school.name.split(' ')[1]}</span>
                  </div>
                ))}
              </div>
            </div>

            <div className="rounded-2xl border border-[#eee6d5] bg-white p-6">
              <h3 className="font-extrabold">Rede na prática</h3>
              <div className="mt-6 space-y-4">
                <Stat value={municipality.schools.length} label="Unidades escolares" />
                <Stat value={totalStudents} label="Matriculados" />
                <Stat value={`${avgFrequency}%`} label="Assiduidade média" />
              </div>
            </div>
          </div>
        </>
      )}

      {view === 'schools' && (
        <>
          <div className="mb-8 flex flex-col justify-between gap-4 sm:flex-row sm:items-end">
            <div>
              <p className="text-sm text-[#8490a4]">Cadastro de unidades</p>
              <h2 className="mt-1 text-3xl font-extrabold">Escolas da rede</h2>
              <p className="mt-2 text-sm text-[#718097]">Visualize e gerencie todas as escolas.</p>
            </div>
            <div className="flex flex-wrap gap-2"><CsvUpload label="Importar escolas CSV" onImport={importSchools} template={{ fileName: 'modelo-escolas.csv', headers: ['nome_escola'], sample: ['EMEF Escola Exemplo'] }} /><button onClick={() => setShowAddSchool(!showAddSchool)} className="flex items-center justify-center gap-2 rounded-xl bg-[#afab50] px-4 py-3 text-sm font-bold text-white">
              <Plus size={16} />
              Adicionar Escola
            </button></div>
          </div>

          {showAddSchool && (
            <form onSubmit={handleAddSchool} className="mb-6 flex gap-3 rounded-2xl border border-[#f3d49e] bg-[#f0f5ff] p-5">
              <input
                autoFocus
                value={newSchool}
                onChange={(e) => setNewSchool(e.target.value)}
                className="flex-1 rounded-lg border border-[#ead9b8] bg-white px-3 py-2.5 text-sm outline-none"
                placeholder="Nome da escola"
              />
              <button className="rounded-lg bg-[#403d28] px-4 py-2.5 text-sm font-bold text-white">Salvar</button>
            </form>
          )}

          <div className="grid gap-4 md:grid-cols-2">
            {municipality.schools.map((school: School) => (
              <div key={school.id} className="rounded-2xl border border-[#eee6d5] bg-white p-6 hover:border-[#afab50] cursor-pointer transition" onClick={() => onSelectSchool(school)}>
                <div className="flex items-start justify-between">
                  <div className="flex-1">
                    <h3 className="font-extrabold">{school.name}</h3>
                    <p className="mt-2 text-sm text-[#718097]">Diretor: {school.director}</p>
                    <div className="mt-4 grid grid-cols-3 gap-4">
                      <div>
                        <p className="text-[12px] text-[#8490a4]">Professores</p>
                        <p className="mt-1 font-bold">{school.teachers}</p>
                      </div>
                      <div>
                        <p className="text-[12px] text-[#8490a4]">Alunos</p>
                        <p className="mt-1 font-bold">{school.students}</p>
                      </div>
                      <div>
                        <p className="text-[12px] text-[#8490a4]">Frequência</p>
                        <p className="mt-1 font-bold">{school.frequency}%</p>
                      </div>
                    </div>
                  </div>
                  <ActionButtons onEdit={(event) => { event?.stopPropagation(); editSchool(school.id) }} onDelete={(event) => { event?.stopPropagation(); deleteSchool(school.id) }} /><span className="text-[#afab50]">→</span>
                </div>
              </div>
            ))}
          </div>
        </>
      )}

      {view === 'directors' && (
        <>
          <div className="mb-8 flex flex-col justify-between gap-4 sm:flex-row sm:items-end">
            <div>
              <p className="text-sm text-[#8490a4]">Gestão de diretores</p>
              <h2 className="mt-1 text-3xl font-extrabold">Diretores da rede</h2>
              <p className="mt-2 text-sm text-[#718097]">Cadastre o responsável e libere o acesso à secretaria de cada escola.</p>
            </div>
            <div className="flex flex-wrap gap-2"><CsvUpload label="Importar diretores CSV" onImport={importDirectors} template={{ fileName: 'modelo-diretores.csv', headers: ['nome_diretor', 'email', 'escola'], sample: ['Maria Silva', 'maria@escola.edu.br', 'EMEF Professor João Ribeiro'] }} /><button onClick={() => setShowAddDirector(!showAddDirector)} className="flex items-center justify-center gap-2 rounded-xl bg-[#afab50] px-4 py-3 text-sm font-bold text-white">
              <Plus size={16} />
              Cadastrar diretor
            </button></div>
          </div>
          {showAddDirector && (
            <form onSubmit={handleAddDirector} className="mb-6 grid gap-3 rounded-2xl border border-[#f3d49e] bg-[#f0f5ff] p-5 md:grid-cols-[1fr_1fr_1fr_auto]">
              <input autoFocus required value={newDirector.name} onChange={(e) => setNewDirector({ ...newDirector, name: e.target.value })} className="rounded-lg border border-[#ead9b8] bg-white px-3 py-2.5 text-sm outline-none" placeholder="Nome completo" />
              <input required type="email" value={newDirector.email} onChange={(e) => setNewDirector({ ...newDirector, email: e.target.value })} className="rounded-lg border border-[#ead9b8] bg-white px-3 py-2.5 text-sm outline-none" placeholder="E-mail de acesso" />
              <select required value={newDirector.school} onChange={(e) => setNewDirector({ ...newDirector, school: e.target.value })} className="rounded-lg border border-[#ead9b8] bg-white px-3 py-2.5 text-sm outline-none">
                <option value="">Selecione a escola</option>
                {municipality.schools.map((school: School) => <option key={school.id} value={school.name}>{school.name}</option>)}
              </select>
              <button className="rounded-lg bg-[#403d28] px-4 py-2.5 text-sm font-bold text-white">Salvar acesso</button>
            </form>
          )}
          <div className="grid gap-4">
            {municipality.directors.map((director: any, i: number) => (
              <div key={i} className="flex items-center gap-4 rounded-2xl border border-[#eee6d5] bg-white p-5">
                <span className="grid size-10 place-items-center rounded-xl bg-[#fff0d8] text-[#afab50]">
                  <Users size={18} />
                </span>
                <div className="flex-1">
                  <p className="text-sm font-bold">{director.name}</p>
                  <p className="mt-1 text-xs text-[#8490a4]">{director.school}</p>
                </div>
                <span className="text-xs font-semibold text-[#21a579]">Ativo</span>
              </div>
            ))}
          </div>
        </>
      )}

      {view === 'reports' && (
        <>
          <div className="mb-8">
            <p className="text-sm text-[#8490a4]">Relatórios estratégicos</p>
            <h2 className="mt-1 text-3xl font-extrabold">Dados da rede municipal</h2>
          </div>
          <div className="grid gap-4 md:grid-cols-3">
            {[
              ['Desempenho da rede', 'Frequência, rendimento e evolução'],
              ['Censo educacional', 'Matrícula e características das escolas'],
              ['Alocação de recursos', 'Distribuição de professores e materiais'],
            ].map(([t, d]) => (
              <div key={t} className="rounded-2xl border border-[#eee6d5] bg-white p-6">
                <FileText className="text-[#afab50]" size={22} />
                <h3 className="mt-6 font-extrabold">{t}</h3>
                <p className="mt-2 text-sm leading-6 text-[#718097]">{d}</p>
                <button className="mt-6 flex items-center gap-2 text-xs font-bold text-[#afab50]">
                  <FileText size={15} />
                  Gerar PDF
                </button>
              </div>
            ))}
          </div>
        </>
      )}
    </Shell>
  )
}

// ============ PERFIL SECRETARIA DA ESCOLA ============
function SchoolApp({ school, municipality, onBack }: any) {
  const [view, setView] = useState<SchoolView>('dashboard')
  const [teachers, setTeachers] = useState(['Ana Paula Mendes', 'Carlos Eduardo Lima', 'Fernanda Alves', 'João Ribeiro'])
  const [classes, setClasses] = useState(['5º Ano A', '5º Ano B', '6º Ano A', '1ª Série A'])
  const [students, setStudents] = useState(['Beatriz Oliveira', 'Lucas Santos', 'Mariana Costa', 'Rafael Almeida', 'Sofia Martins'])
  const disciplines = ['Língua Portuguesa', 'Matemática', 'Geografia', 'História', 'Ciências', 'Arte', 'Religião', 'Inglês', 'Educação Física', 'Diversificada']
  const [teacherDisciplines, setTeacherDisciplines] = useState<Record<string, string[]>>({
    'Ana Paula Mendes': ['Língua Portuguesa', 'História'],
    'Carlos Eduardo Lima': ['Matemática'],
    'Fernanda Alves': ['Ciências', 'Geografia'],
    'João Ribeiro': [],
  })
  const [showForm, setShowForm] = useState(false)
  const [newItem, setNewItem] = useState('')
  const [search, setSearch] = useState('')

  const items = [
    { id: 'dashboard', label: 'Visão geral', icon: LayoutDashboard },
    { id: 'teachers', label: 'Professores', icon: Users },
    { id: 'classes', label: 'Turmas', icon: ClipboardList },
    { id: 'students', label: 'Alunos', icon: GraduationCap },
    { id: 'reports', label: 'Relatórios', icon: FileText },
  ]

  const handleAdd = (e: any) => {
    e.preventDefault()
    if (newItem.trim()) {
      if (view === 'teachers') setTeachers([...teachers, newItem])
      if (view === 'classes') setClasses([...classes, newItem])
      if (view === 'students') setStudents([...students, newItem])
      setNewItem('')
      setShowForm(false)
    }
  }

  const importItems = (items: string[]) => { if (view === 'teachers') setTeachers((current) => [...current, ...items]); if (view === 'classes') setClasses((current) => [...current, ...items]); if (view === 'students') setStudents((current) => [...current, ...items]) }
  const editItem = (type: 'teachers' | 'classes' | 'students', index: number) => {
    const list = type === 'teachers' ? teachers : type === 'classes' ? classes : students
    const next = window.prompt('Editar cadastro:', list[index])?.trim()
    if (!next) return
    if (type === 'teachers') { setTeachers((current) => current.map((item, itemIndex) => itemIndex === index ? next : item)); setTeacherDisciplines((current) => { const result = { ...current, [next]: current[list[index]] || [] }; delete result[list[index]]; return result }) }
    if (type === 'classes') setClasses((current) => current.map((item, itemIndex) => itemIndex === index ? next : item))
    if (type === 'students') setStudents((current) => current.map((item, itemIndex) => itemIndex === index ? next : item))
  }
  const deleteItem = (type: 'teachers' | 'classes' | 'students', index: number) => {
    if (!window.confirm('Excluir este cadastro?')) return
    if (type === 'teachers') { const name = teachers[index]; setTeachers((current) => current.filter((_, itemIndex) => itemIndex !== index)); setTeacherDisciplines((current) => { const result = { ...current }; delete result[name]; return result }) }
    if (type === 'classes') setClasses((current) => current.filter((_, itemIndex) => itemIndex !== index))
    if (type === 'students') setStudents((current) => current.filter((_, itemIndex) => itemIndex !== index))
  }
  const toggleTeacherDiscipline = (teacher: string, discipline: string) => {
    setTeacherDisciplines((current) => {
      const assigned = current[teacher] || []
      const next = assigned.includes(discipline) ? assigned.filter((item) => item !== discipline) : [...assigned, discipline]
      return { ...current, [teacher]: next }
    })
  }
  const filteredStudents = useMemo(() => students.filter((s) => s.toLowerCase().includes(search.toLowerCase())), [students, search])

  return (
    <Shell title={`${school.name}`} view={view} setView={setView} items={items} onBack={onBack} accessScope="Acesso à Secretaria da Escola atual e aos Professores, Turmas e Alunos vinculados a ela.">
      {view === 'dashboard' && (
        <>
          <div className="mb-8">
            <p className="text-sm text-[#8490a4]">Quarta-feira, 17 de setembro de 2026</p>
            <h2 className="mt-1 text-3xl font-extrabold tracking-[-.04em]">Bom dia, Secretaria.</h2>
            <p className="mt-2 text-sm text-[#718097]">Acompanhe a rotina da {school.name}.</p>
          </div>

          <div className="grid gap-4 sm:grid-cols-2 xl:grid-cols-4">
            <Card icon={Users} value={teachers.length} label="Professores ativos" />
            <Card icon={ClipboardList} value={classes.length} label="Turmas cadastradas" />
            <Card icon={GraduationCap} value={students.length} label="Alunos matriculados" />
            <Card icon={CalendarCheck} value="93,8%" label="Frequência média" />
          </div>

          <div className="mt-6 grid gap-6 xl:grid-cols-[1.3fr_.7fr]">
            <div className="rounded-2xl border border-[#eee6d5] bg-white p-6">
              <div className="flex justify-between">
                <div>
                  <h3 className="font-extrabold">Frequência da escola</h3>
                  <p className="mt-1 text-xs text-[#8490a4]">Média dos últimos meses</p>
                </div>
                <BarChart3 className="text-[#afab50]" size={20} />
              </div>
              <div className="mt-8 flex h-48 items-end gap-3">
                {[76, 82, 80, 88, 86, 92, 90, 94].map((h, i) => (
                  <div key={i} className="flex flex-1 flex-col items-center gap-2">
                    <div className="w-full rounded-t-lg bg-[#fcbf6b]" style={{ height: `${h * 1.6}px`, maxHeight: 150 }} />
                    <span className="text-[10px] text-[#9aa4b4]">{['Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out'][i]}</span>
                  </div>
                ))}
              </div>
            </div>

            <div className="rounded-2xl border border-[#eee6d5] bg-white p-6">
              <h3 className="font-extrabold">Informações da escola</h3>
              <div className="mt-6 space-y-4">
                <Stat value={school.director} label="Diretor" />
                <Stat value={teachers.length} label="Corpo docente" />
                <Stat value={students.length} label="Matriculados" />
              </div>
            </div>
          </div>
        </>
      )}

      {view !== 'dashboard' && view !== 'reports' && (
        <>
          <div className="mb-8 flex flex-col justify-between gap-4 sm:flex-row sm:items-end">
            <div>
              <p className="text-sm text-[#8490a4]">Cadastro e acompanhamento</p>
              <h2 className="mt-1 text-3xl font-extrabold">{view === 'teachers' ? 'Professores' : view === 'classes' ? 'Turmas' : 'Alunos'}</h2>
              <p className="mt-2 text-sm text-[#718097]">Mantenha os dados da escola sempre atualizados.</p>
            </div>
            <div className="flex flex-wrap gap-2"><CsvUpload label="Importar CSV" onImport={importItems} template={{ fileName: `modelo-${view}.csv`, headers: [view === 'teachers' ? 'nome_professor' : view === 'classes' ? 'nome_turma' : 'nome_aluno'], sample: [view === 'teachers' ? 'Ana Paula Mendes' : view === 'classes' ? '5º Ano A' : 'Beatriz Oliveira'] }} /><button onClick={() => setShowForm(!showForm)} className="flex items-center justify-center gap-2 rounded-xl bg-[#afab50] px-4 py-3 text-sm font-bold text-white">
              <Plus size={16} />
              Adicionar
            </button></div>
          </div>

          {showForm && (
            <form onSubmit={handleAdd} className="mb-6 flex gap-3 rounded-2xl border border-[#f3d49e] bg-[#f0f5ff] p-5">
              <input
                autoFocus
                value={newItem}
                onChange={(e) => setNewItem(e.target.value)}
                className="flex-1 rounded-lg border border-[#ead9b8] bg-white px-3 py-2.5 text-sm outline-none"
                placeholder={`Nome de ${view === 'teachers' ? 'professor' : view === 'classes' ? 'turma' : 'aluno'}`}
              />
              <button className="rounded-lg bg-[#403d28] px-4 py-2.5 text-sm font-bold text-white">Salvar</button>
            </form>
          )}

  {view === 'students' && (
  <div className="mb-5 flex items-center gap-3 rounded-xl border border-[#eee6d5] bg-white px-4 py-3">
  <Search size={17} className="text-[#9aa4b4]" />
  <input value={search} onChange={(e) => setSearch(e.target.value)} className="w-full text-sm outline-none" placeholder="Buscar aluno..." />
  </div>
  )}

  {view === 'teachers' && (
  <div className="mb-6 rounded-2xl border border-[#ead9b8] bg-white p-6">
  <div className="mb-5 flex items-start justify-between gap-4">
  <div><h3 className="font-extrabold">Disciplinas por professor</h3><p className="mt-1 text-sm text-[#718097]">Selecione uma ou mais disciplinas para cada professor. Essas atribuições aparecem no perfil do professor.</p></div>
  <BookOpen className="text-[#afab50]" size={22} />
  </div>
  <div className="grid gap-4 xl:grid-cols-2">
  {teachers.map((teacher) => (
  <div key={teacher} className="rounded-xl border border-[#edf0f5] bg-[#f9fbff] p-4">
  <div className="mb-3 flex items-center justify-between"><span className="font-bold text-sm">{teacher}</span><span className="text-xs font-semibold text-[#afab50]">{(teacherDisciplines[teacher] || []).length} atribuída(s)</span></div>
  <div className="grid grid-cols-2 gap-2 sm:grid-cols-3">
  {disciplines.map((discipline) => { const checked = (teacherDisciplines[teacher] || []).includes(discipline); return <label key={discipline} className={`flex cursor-pointer items-center gap-2 rounded-lg border px-2.5 py-2 text-xs font-semibold transition ${checked ? 'border-[#9fbeff] bg-[#fff0d8] text-[#afab50]' : 'border-[#eee6d5] bg-white text-[#718097]'}`}><input type="checkbox" checked={checked} onChange={() => toggleTeacherDiscipline(teacher, discipline)} className="accent-[#afab50]" />{discipline}</label> })}
  </div>
  </div>
  ))}
  </div>
  </div>
  )}
  
  <div className="grid gap-3">
            {(view === 'students' ? filteredStudents : view === 'teachers' ? teachers : classes).map((item: string, i: number) => (
              <div key={`${item}-${i}`} className="flex items-center gap-4 rounded-2xl border border-[#eee6d5] bg-white p-5">
                <span className="grid size-10 place-items-center rounded-xl bg-[#fff0d8] text-[#afab50]">
                  <Users size={18} />
                </span>
                <span className="flex-1 text-sm font-bold">{item}</span>
                <span className="text-xs font-semibold text-[#21a579]">Ativo</span>
                <ActionButtons onEdit={() => editItem(view as 'teachers' | 'classes' | 'students', i)} onDelete={() => deleteItem(view as 'teachers' | 'classes' | 'students', i)} />
              </div>
            ))}
          </div>
        </>
      )}

      {view === 'reports' && (
        <>
          <div className="mb-8">
            <p className="text-sm text-[#8490a4]">Dados para decisão</p>
            <h2 className="mt-1 text-3xl font-extrabold">Relatórios da escola</h2>
          </div>
          <div className="grid gap-4 md:grid-cols-3">
            {[['Frequência escolar', 'Acompanhe presença por turma'], ['Desempenho das turmas', 'Notas e evolução'], ['Censo escolar', 'Resumo cadastral da unidade']].map(([t, d]) => (
              <div key={t} className="rounded-2xl border border-[#eee6d5] bg-white p-6">
                <FileText className="text-[#afab50]" size={22} />
                <h3 className="mt-6 font-extrabold">{t}</h3>
                <p className="mt-2 text-sm leading-6 text-[#718097]">{d}</p>
                <button className="mt-6 flex items-center gap-2 text-xs font-bold text-[#afab50]">
                  <FileText size={15} />
                  Gerar PDF
                </button>
              </div>
            ))}
          </div>
        </>
      )}
    </Shell>
  )
}

// ============ PERFIL PROFESSOR ============
function TeacherApp({ teacher, onBack }: any) {
  const [view, setView] = useState<TeacherView>('dashboard')
  const [activeClass, setActiveClass] = useState<TeacherClass | null>(null)
  const [attendance, setAttendance] = useState<Record<string, boolean>>({ Beatriz: true, Lucas: true, Mariana: false, Rafael: true })
  const [savedAttendance, setSavedAttendance] = useState(false)
  const students = ['Beatriz', 'Lucas', 'Mariana', 'Rafael']
  const disciplines = ['Língua Portuguesa', 'Matemática', 'Geografia', 'História', 'Ciências', 'Arte', 'Religião', 'Inglês', 'Educação Física', 'Diversificada']
  const [selectedDiscipline, setSelectedDiscipline] = useState(disciplines[0])
  const [grades, setGrades] = useState<Record<string, number[]>>({ Beatriz: [2, 2, 2, 2, 1.5, 2, 1.5, 2, 2, 2, 2, 2], Lucas: [1.5, 2, 1, 1.5, 2, 1.5, 1.5, 2, 1.5, 2, 1.5, 1.5], Mariana: [1, 1, 1.5, 1, 1.5, 1, 1, 1.5, 1, 1, 1, 1.5], Rafael: [2.5, 2.5, 2.5, 2.5, 2.5, 2.5, 2.5, 2.5, 2.5, 2.5, 2.5, 2.5] })
  const items = [
    { id: 'dashboard', label: 'Visão geral', icon: LayoutDashboard }, { id: 'attendance', label: 'Chamadas', icon: CalendarCheck },
    { id: 'lessons', label: 'Planos de aula', icon: BookOpen }, { id: 'grades', label: 'Notas', icon: ClipboardList }, { id: 'planning', label: 'Planejamento', icon: BarChart3 },
  ]
  const setGrade = (student: string, index: number, value: string) => setGrades((current) => ({ ...current, [student]: current[student].map((grade, i) => i === index ? Math.min(10, Math.max(0, Number(value))) : grade) }))
  const unitTotal = (student: string, unit: number) => { const values = grades[student].slice(unit * 4, unit * 4 + 4); return Math.min(10, values.reduce((sum, value) => sum + value, 0)) }
  const finalAverage = (student: string) => [0, 1, 2].reduce((sum, unit) => sum + unitTotal(student, unit), 0) / 3
  const finalStatus = (student: string) => finalAverage(student) >= 5 ? 'Aprovado' : 'Conservado'
  return (
    <Shell title={`Prof. ${teacher.name}`} view={view} setView={setView} items={items} onBack={onBack} accessScope="Acesso somente à própria área: Chamadas, Planos de aula, Notas e Planejamento das suas turmas.">
      {view === 'dashboard' && <><div className="mb-8"><p className="text-sm text-[#8490a4]">Quarta-feira, 17 de setembro de 2026</p><h2 className="mt-1 text-3xl font-extrabold">Bem-vindo, {teacher.name.split(' ')[0]}!</h2><p className="mt-2 text-sm text-[#718097]">Acompanhe suas turmas, chamadas e avaliações.</p></div><div className="grid gap-4 sm:grid-cols-3"><Card icon={ClipboardList} value={teacher.classes.length} label="Turmas" /><Card icon={GraduationCap} value={teacher.classes.reduce((sum: number, c: TeacherClass) => sum + c.students, 0)} label="Alunos" /><Card icon={CalendarCheck} value="3" label="Unidades letivas" /></div></>}
      {view === 'attendance' && <><div className="mb-8"><p className="text-sm text-[#8490a4]">Registro diário</p><h2 className="mt-1 text-3xl font-extrabold">Chamadas</h2><p className="mt-2 text-sm text-[#718097]">Selecione uma turma para registrar a presença de cada aluno.</p></div>{!activeClass ? <div className="grid gap-4 md:grid-cols-2">{teacher.classes.map((cls: TeacherClass) => <div key={cls.id} className="rounded-2xl border border-[#eee6d5] bg-white p-6"><p className="font-bold">{cls.name}</p><p className="mt-1 text-sm text-[#8490a4]">{cls.students} alunos</p><button onClick={() => { setActiveClass(cls); setSavedAttendance(false) }} className="mt-5 rounded-lg bg-[#afab50] px-4 py-2 text-sm font-bold text-white">Fazer chamada</button></div>)}</div> : <div className="rounded-2xl border border-[#eee6d5] bg-white p-6"><div className="flex flex-wrap items-center justify-between gap-3"><div><h3 className="font-extrabold">{activeClass.name}</h3><p className="text-sm text-[#8490a4]">17/09/2026 · marque quem está presente</p></div><button onClick={() => setActiveClass(null)} className="text-sm font-bold text-[#68768c]">Trocar turma</button></div><div className="mt-6 grid gap-3">{students.map((student) => <label key={student} className="flex items-center justify-between rounded-xl bg-[#fffaf2] px-4 py-3"><span className="font-semibold">{student}</span><input type="checkbox" checked={!!attendance[student]} onChange={(event) => setAttendance({ ...attendance, [student]: event.target.checked })} className="size-5 accent-[#afab50]" /></label>)}</div><button onClick={() => setSavedAttendance(true)} className="mt-6 rounded-lg bg-[#21a579] px-5 py-3 text-sm font-bold text-white">Salvar chamada</button>{savedAttendance && <p className="mt-3 text-sm font-semibold text-[#21a579]">Chamada salva com sucesso.</p>}</div>}</>}
      {view === 'grades' && <><div className="mb-8"><p className="text-sm text-[#8490a4]">Avaliação contínua</p><h2 className="mt-1 text-3xl font-extrabold">Notas dos alunos</h2><p className="mt-2 text-sm text-[#718097]">Selecione a disciplina. Cada unidade possui 4 avaliações e total final máximo de 10 pontos. Média mínima: 5,0.</p></div><div className="mb-4 flex flex-wrap items-center gap-3 rounded-2xl border border-[#ead9b8] bg-white p-4"><label htmlFor="discipline" className="text-sm font-bold text-[#64738d]">Disciplina</label><select id="discipline" value={selectedDiscipline} onChange={(event) => setSelectedDiscipline(event.target.value)} className="rounded-xl border border-[#dbe3ef] bg-white px-4 py-2.5 text-sm font-semibold text-[#403d28]">{disciplines.map((discipline) => <option key={discipline}>{discipline}</option>)}</select><span className="text-xs text-[#8490a4]">{selectedDiscipline} · 3 unidades letivas</span></div><div className="rounded-2xl border border-[#eee6d5] bg-white p-4"><div className="overflow-x-auto"><table className="w-full min-w-[980px]"><thead className="bg-[#fffaf2]"><tr><th className="px-3 py-3 text-left text-xs font-bold text-[#8490a4]">Aluno</th>{[1,2,3].map((unit) => <th key={unit} colSpan={5} className="px-3 py-3 text-center text-xs font-bold text-[#afab50]">Unidade {unit} · máx. 10</th>)}</tr><tr><th /><>{[1,2,3].flatMap((unit) => [...[1,2,3,4].map((evaluation) => <th key={`${unit}-${evaluation}`} className="px-2 py-2 text-xs text-[#8490a4]">A{evaluation}</th>), <th key={`m-${unit}`} className="px-2 py-2 text-xs text-[#8490a4]">Total</th>])}</></tr></thead><tbody>{students.map((student) => <tr key={student} className="border-t border-[#eee6d5]"><td className="px-3 py-3 text-sm font-bold">{student}</td>{[0,1,2].flatMap((unit) => [...grades[student].slice(unit * 4, unit * 4 + 4).map((grade, index) => <td key={`${unit}-${index}`} className="px-2 py-3"><input aria-label={`${student} ${selectedDiscipline} Unidade ${unit + 1} avaliação ${index + 1}`} type="number" min="0" max="10" step="0.1" value={grade} onChange={(event) => setGrade(student, unit * 4 + index, event.target.value)} className="w-14 rounded border border-[#dbe3ef] px-2 py-1 text-center text-sm" /></td>), <td key={`total-${unit}`} className={`px-2 py-3 text-center text-sm font-extrabold ${unitTotal(student, unit) >= 5 ? 'text-[#21a579]' : 'text-[#d95b5b]'}`}>{unitTotal(student, unit).toFixed(1)} / 10</td>])}</tr>)}</tbody></table></div><div className="mt-6 rounded-2xl border border-[#ead9b8] bg-[#fffaf2] p-4"><h3 className="font-extrabold text-[#403d28]">Resultado final por aluno</h3><p className="mt-1 text-xs text-[#8490a4]">Soma das 3 unidades dividida por 3. Aprovação com média final maior ou igual a 5,0.</p><div className="mt-4 grid gap-3 md:grid-cols-2">{students.map((student) => { const average = finalAverage(student); return <div key={`final-${student}`} className="flex items-center justify-between rounded-xl border border-[#eee6d5] bg-white px-4 py-3"><div><p className="font-bold">{student}</p><p className="text-xs text-[#8490a4]">U1 {unitTotal(student, 0).toFixed(1)} + U2 {unitTotal(student, 1).toFixed(1)} + U3 {unitTotal(student, 2).toFixed(1)}</p></div><div className="text-right"><p className="text-lg font-extrabold text-[#403d28]">{average.toFixed(1)}</p><span className={`text-xs font-bold ${average >= 5 ? 'text-[#21a579]' : 'text-[#d26a3a]'}`}>{finalStatus(student)}</span></div></div>})}</div></div></div></>}
      {view === 'lessons' && <><div className="mb-8"><p className="text-sm text-[#8490a4]">Organização pedagógica</p><h2 className="mt-1 text-3xl font-extrabold">Planos de aula</h2></div><div className="grid gap-4 md:grid-cols-2">{[1,2,3,4].map((i) => <div key={i} className="rounded-2xl border border-[#eee6d5] bg-white p-6"><p className="font-bold">Aula {i}: Tema Pedagógico</p><p className="mt-2 text-sm text-[#8490a4]">Turma: 5�� Ano A</p><span className="mt-3 block text-xs font-semibold text-[#21a579]">Pronto</span></div>)}</div></>}
      {view === 'planning' && <><div className="mb-8"><p className="text-sm text-[#8490a4]">Unidades e currículo</p><h2 className="mt-1 text-3xl font-extrabold">Planejamento anual</h2></div><div className="grid gap-4">{['Números e Operações','Espaço e Formas','Tratamento da Informação'].map((unit, i) => <div key={unit} className="rounded-2xl border border-[#eee6d5] bg-white p-6"><p className="font-bold">Unidade {i + 1}: {unit}</p><p className="mt-2 text-sm text-[#8490a4]">4 avaliações previstas · média mínima 5,0</p></div>)}</div></>}
    </Shell>
  )
}

// ============ COMPONENTES AUXILIARES ============
function ActionButtons({ onEdit, onDelete }: { onEdit: (event?: React.MouseEvent<HTMLButtonElement>) => void; onDelete: (event?: React.MouseEvent<HTMLButtonElement>) => void }) {
  return <div className="flex items-center gap-1" onClick={(event) => event.stopPropagation()}>
    <button type="button" aria-label="Editar cadastro" title="Editar" onClick={onEdit} className="grid size-9 place-items-center rounded-lg text-[#64738d] hover:bg-[#fff0d8] hover:text-[#afab50]"><Pencil size={16} /></button>
    <button type="button" aria-label="Excluir cadastro" title="Excluir" onClick={onDelete} className="grid size-9 place-items-center rounded-lg text-[#64738d] hover:bg-[#fff1f0] hover:text-[#d95b5b]"><Trash2 size={16} /></button>
  </div>
}

function Card({ icon: Icon, value, label }: any) {
  return (
    <div className="rounded-2xl border border-[#eee6d5] bg-white p-5">
      <span className="grid size-10 place-items-center rounded-xl bg-[#fff0d8] text-[#afab50]">
        <Icon size={18} />
      </span>
      <strong className="mt-4 block text-2xl font-extrabold">{value}</strong>
      <span className="text-xs font-semibold text-[#8490a4]">{label}</span>
    </div>
  )
}

function Stat({ value, label }: any) {
  return (
    <div className="flex justify-between">
      <span className="text-sm text-[#8490a4]">{label}</span>
      <span className="font-bold">{value}</span>
    </div>
  )
}

void Check; void Settings; void X
